from flask import Flask, request, redirect, render_template, flash
from mysqlconnection import MySQLConnector
import re

EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')
app = Flask(__name__)
mysql= MySQLConnector(app, 'email_validation')
app.secret_key = "ExtraSuperSecretKey"

@app.route('/')
def index():
    return render_template('index.html')

@app.route('/success', methods = ['POST'])
def email():
    if len(request.form['email']) < 1:
        flash("you didn't enter an email address!")
    elif not EMAIL_REGEX.match(request.form['email']):
        flash("that's not a valid email address!")
    else:
        query = "SELECT * FROM users WHERE email = :email"
        data = {
            'email': request.form['email']
                }
        result = mysql.query_db(query, data)
        if len(result) >=1:
            flash("that email is already in our system!")
        else:
            query = "INSERT INTO users (email, created_at, updated_at) VALUES(:email, NOW(), NOW())"
            data = {
                'email': request.form['email']
                    }
            mysql.query_db(query, data)
            all_users = mysql.query_db("SELECT * FROM users")
            email = request.form['email']
            return render_template('success.html', all_users = all_users, email = email)
    return redirect('/')
app.run(debug=True)