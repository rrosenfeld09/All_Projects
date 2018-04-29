# TO-DO:
# - clean code (remove old comments and add new ones)
# - add bycrpt 
# - add delete button to comments and posts (if made my the user that is signed in)
# - only allow users to delete comment/post if it was made less than 30 mins ago

"""ABOUT ME: On the root route, this application allows user to either create an account
or sign into an existing account (using standard form validation and bcrypt password encryption).
Once signed in, users can post messages to a communal wall, and comment on the posts of others."""


from flask import Flask, render_template, redirect, request, session, flash
from mysqlconnection import MySQLConnection
# from flask.ext.bcrypt import Bcrypt
import re
import md5

EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
app = Flask(__name__)
mysql = MySQLConnection(app, 'the_wall')
app.secret_key="SuperSecretKey"
# bcrypt = Bcrypt(app)

#THIS IS THE LANDING PAGE (IF A USER IS ALREADY SIGNED IN, THEY ARE REDIRECTED TO THE WALL)
@app.route("/")
def index():
    if session['id'] != 0:
        return redirect("/wall")
    return render_template("index.html")

#REGISTRATION W/ FROM VALIDATION
@app.route("/register1", methods=["POST"])
def register():
    query_check_unique = "SELECT * FROM users WHERE email = :email"
    data_check_unique = {
        'email':request.form['email']
        }
    check = mysql.query_db(query_check_unique, data_check_unique)
    if len(request.form["first_name"]) < 1:
        flash("woops! you didn't type in your first name!")
    elif request.form["first_name"].isalpha() == False:
        flash("your first name cannot contain a number!")
    elif len(request.form["last_name"]) < 1:
        flash("woops! you didn't type in your last name!")
    elif request.form["last_name"].isalpha() == False:
        flash("your last name cannot contain a number!")
    elif len(request.form["email"]) < 1:
        flash("woops! you didn't type in your email!")
    elif not EMAIL_REGEX.match(request.form['email']):
        flash("please enter a valid email address!")
    elif len(request.form["password"]) < 1:
        flash("you forgot to enter a password!")
    elif len(request.form["password"]) < 8:
        flash("your password must be at least 8 characters long!")
    elif request.form["password"] != request.form["confirm_password"]:
        flash("your passwords dont match!")
    elif len(check) >= 1:
            flash("that email is already in our system, please sign in!")
    else:
        query = "INSERT INTO users (first_name, last_name, email, password, created_at, updated_at) VALUES(:first_name, :last_name, :email, :password, NOW(), NOW())"
        data = {
            'first_name':request.form["first_name"],
            'last_name':request.form["last_name"],
            'email':request.form["email"],
            'password':md5.new(request.form["password"]).hexdigest()
        }
        mysql.query_db(query, data)

        query_success = "SELECT * FROM users WHERE email = :email"
        data_success = {
            'email':request.form["email"]
        }
        success = mysql.query_db(query_success, data_success)
        session['id'] = success[0]['id']
        print session['id']
        print "ADDED USER"
        return redirect("/wall")
    return redirect("/")

#THIS ALLOWS USERS WITH EXISTING ACCOUNTS TO SIGN IN
@app.route("/login", methods = ["POST"])
def login():
    query_check_unique = "SELECT * FROM users WHERE email = :email"
    data_check_unique = {
        'email':request.form['email']
        }
    check = mysql.query_db(query_check_unique, data_check_unique)

    if not EMAIL_REGEX.match(request.form['email']):
        flash("please enter a valid email address")
    elif len(check) <1:
        flash("that email isn't signed up, please register!")
    else:
        query = "SELECT * FROM users WHERE email = :email"
        data = {
            'email':request.form["email"]
        }
        check = mysql.query_db(query, data)
        if md5.new(request.form["password"]).hexdigest() == check[0]['password']:      
            session['id'] = check[0]['id']
            print session['id']
            print "LOGGED IN"
            return redirect("/wall")
        else:
            flash("that's the wrong password!")
    return redirect("/")

#THIS IS THE HOMEPAGE OF THE APPLICATION
@app.route("/wall")
def home():
    if session['id'] == 0:
        return redirect("/")
    query = "SELECT first_name from users WHERE id = :id"
    data = {
        'id': session['id']
    }
    customize = mysql.query_db(query, data)
    name = customize[0]['first_name']

    messages_query = "SELECT CONCAT(first_name, ' ', last_name) as name, message, m.id as message_id, DATE_FORMAT(m.created_at, '%M %D %Y') as date, user_id FROM messages m JOIN users u ON m.user_id =u.id ORDER BY m.created_at DESC"
    messages = mysql.query_db(messages_query)

    comments_query = "SELECT CONCAT(first_name, ' ', last_name) as name, comment, c.user_id, c.message_id, m.id as message_id, DATE_FORMAT(c.created_at, '%M %D %Y') as date, c.user_id FROM comments c JOIN users u ON u.id = c.user_id JOIN messages m ON m.id = c.message_id"
    comments = mysql.query_db(comments_query)
    # for message in messages:
    #     print message['name']
    #     print message['message']
    #     print message['created_at']
    # for comment in comments:
    #     print comment['comment']

    return render_template("homepage.html", name = name, messages = messages, comments = comments)

#THIS ALLOWS USERS TO CREATE A NEW MESSAGE AND POST IT TO THE WALL
@app.route('/add_message', methods=["POST"])
def add_message():
    if len(request.form['text_box']) > 0:
        query_message = "INSERT INTO messages (user_id, message, created_at, updated_at) VALUES(:user_id, :message, NOW(), NOW())"
        data_message = {
            'user_id': session['id'],
            'message': request.form['text_box']
        }
        mysql.query_db(query_message, data_message)
    return redirect("/wall")

#THIS ALLOWS USERS TO COMMENT ON THE POSTS OF OTHERS (OR ON THEIR OWN)
@app.route("/add_comment/<message_id>", methods=["POST"])
def add_comment(message_id):
    if len(request.form['comment_box']) > 0:
        query_comment = "INSERT INTO comments (user_id, message_id, comment, created_at, updated_at) VALUES(:user_id, :message_id, :comment, NOW(), NOW())"
        data_comment = {
            'user_id': session['id'],
            'message_id': message_id,
            'comment': request.form['comment_box'] 
        }
        mysql.query_db(query_comment, data_comment)
        print "____ADDED COMMENT____"
    return redirect("/wall")

#THIS ALLOWS USERS TO SIGN OUT OF THEIR ACCOUNT
@app.route("/sign_out", methods=["POST"])
def sign_out():
    session['id']=0
    print session['id']
    print "SIGNED OUT"
    return redirect('/')

app.run(debug = True)