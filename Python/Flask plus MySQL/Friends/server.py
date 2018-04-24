from flask import Flask, request, redirect, render_template, session, flash
from mysqlconnection import MySQLConnector
app = Flask(__name__)
mysql = MySQLConnector(app, 'full_friendsdb')

@app.route('/')
def index():
    full_friends = mysql.query_db("SELECT * FROM friends")
    return render_template("index.html", full_friends=full_friends)

@app.route('/full_friends', methods=['POST'])
def full_friends():
    query = "INSERT INTO friends (first_name, last_name, occupation, created_at, updated_at) VALUES (:first_name, :last_name, :occupation, NOW(), NOW())"
    data = {
            'first_name': request.form['first_name'],
            'last_name':  request.form['last_name'],
            'occupation': request.form['occupation']
           }
    mysql.query_db(query, data)
    print "ADDED FRIEND"
    return redirect('/')

@app.route('/remove_friend', methods=['POST'])
def remove_friend():
    query = "DELETE FROM friends WHERE id = :id;"
    data = {
        'id': request.form['id'],
    }
    mysql.query_db(query, data)
    print "REMOVED FRIEND"
    return redirect('/')
app.run(debug=True)
