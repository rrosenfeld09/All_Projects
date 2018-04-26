from flask import Flask, render_template, redirect, request, session, flash
from mysqlconnection import MySQLConnection
import re
import md5

EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
app = Flask(__name__)
mysql = MySQLConnection(app, 'login_and_registration')
app.secret_key="SuperSecretKey"


@app.route("/")
def index():
    return render_template("index.html")

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
        return redirect("/home")
    return redirect("/")

@app.route("/login", methods = ["POST"])
def login():
    query_check_unique = "SELECT * FROM users WHERE email = :email"
    data_check_unique = {
        'email':request.form['email']
        }
    check = mysql.query_db(query_check_unique, data_check_unique)
    session['id'] = check[0]['id']

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
            print session['id']
            print "LOGGED IN"
            return redirect("/home")
        else:
            flash("that's the wrong password!")
    return redirect("/")

@app.route('/home')
def home():
    query = "SELECT first_name from users WHERE id = :id"
    data = {
        'id': session['id']
    }
    customize = mysql.query_db(query, data)
    name = customize[0]['first_name']
    print '___' + name + '___'
    return render_template("homepage.html", name = name)

app.run(debug = True)