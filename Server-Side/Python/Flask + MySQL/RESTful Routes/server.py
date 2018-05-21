"""ABOUT ME: This application displays the conventional naming rules for routing in Flask"""

from flask import Flask, render_template, request, redirect, session, flash
from mysqlconnection import MySQLConnection

app = Flask(__name__)
mysql = MySQLConnection(app, 'full_friendsdb')
app.secret_key = "SuperDuperSecretKey"

@app.route("/users")
def index():
    query = "SELECT id, CONCAT(first_name, ' ', last_name) as full_name, email, DATE_FORMAT(created_at, '%M %D, %Y') as date_created FROM friends"
    friends = mysql.query_db(query)
    return render_template("index.html", all_friends = friends)

@app.route("/users/new")
def new():
    return render_template("new_user.html")

@app.route("/users/create", methods=["POST"])
def create():
    query = "INSERT INTO friends (first_name, last_name, email, created_at, updated_at) VALUES (:first_name, :last_name, :email, NOW(), NOW())"
    data = {
        'first_name': request.form['first_name'],
        'last_name': request.form['last_name'],
        'email': request.form['email']
    }
    mysql.query_db(query, data)

    id_query = "SELECT * FROM friends ORDER BY created_at DESC LIMIT 1"
    info = mysql.query_db(id_query)
    new_id= info[0]['id']
    print new_id
    return redirect("users/{}".format(new_id))

@app.route("/users/<id>")
def show(id):
    query = "SELECT id, CONCAT(first_name, ' ', last_name) as full_name, email, DATE_FORMAT(created_at, '%M %D, %Y') as date_created FROM friends WHERE id = :id"
    data = {
        'id': id
    }
    user_info = mysql.query_db(query, data)
    print user_info[0]['full_name']
    return render_template("show.html", id = id, user_info = user_info)

@app.route("/users/<id>/edit")
def edit(id):
    query = "SELECT id, CONCAT(first_name, ' ', last_name) as full_name, email, DATE_FORMAT(created_at, '%M %D, %Y') as date_created FROM friends WHERE id = :id"
    data = {
        'id': id
    }
    user_info = mysql.query_db(query, data)
    print user_info[0]['id']
    return render_template("edit.html", user_info = user_info)

@app.route("/users/<id>/edit", methods = ["POST"])
def update(id):
    query = "UPDATE friends SET first_name = :first_name, last_name = :last_name, email = :email, updated_at = NOW() WHERE id = :id" 
    data = {
        'first_name': request.form['first_name'],
        'last_name': request.form['last_name'],
        'email': request.form['email'],
        'id': id
    }
    rules = [len(request.form['first_name']) > 0, 
            len(request.form['last_name']) > 0, 
            len(request.form['email']) > 0 ]
    if all(rules):
        mysql.query_db(query, data)
        print "UPDATED"
    else:
        print "failed to update. reason: blank entry"
    return redirect("/users/{}".format(id))
@app.route("/users/<id>/destroy")
def destroy(id):
    query = "DELETE FROM friends WHERE id = :id"
    data = {
        'id': id
    }
    mysql.query_db(query, data)
    return redirect("/users")


app.run(debug=True)