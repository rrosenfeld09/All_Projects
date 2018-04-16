from flask import Flask, redirect, render_template, flash, request

import re

EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')

app = Flask(__name__)
app.secret_key = "SuperDuperSecret"

@app.route("/")
def index():
	return render_template("index.html")

@app.route("/process", methods = ["POST"])
def process():
	if len(request.form['email']) < 1:
		flash("you didn't enter an email address!")
	elif not EMAIL_REGEX.match(request.form['email']):
		flash("that's not a valid email address!")
	elif len(request.form['first_name']) < 1:
		flash("you didn't enter a first name!")
	elif request.form['first_name'].isalpha() != True:
		flash("you first name cannot contain a number")
	elif len(request.form['last_name']) < 1:
		flash("you didn't enter a last name!")
	elif request.form['last_name'].isalpha() != True:
		flash("you last name cannot contain a number")
	elif len(request.form['password']) < 1:
		flash("you didn't enter a password!")
	elif len(request.form['password']) < 8:
		flash("password must be longer than 8 characters")
	elif request.form['password'].islower() == True:
		flash("your password must contain at least 1 uppercase letter!")
	elif request.form['password'].isalpha() == True:
		flash("your password must contain at least 1 number!")
	elif len(request.form['confirm_password']) < 1:
		flash("you didn't confirm your password!")
	elif request.form['password'] != request.form['confirm_password']:
		flash("your passwords don't match!")
	else:
		flash("success!")
	print "registration successful"
	return redirect("/")

app.run(debug=True)