from flask import Flask, render_template, request, redirect, flash

import re

EMAIL_REGEX = re.compile(r'^[a-zA-Z0-9copy.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$')

app = Flask(__name__)
app.secret_key = "MySuperSecretKey"
@app.route('/')
def index():
	return render_template("index.html")

@app.route('/results', methods = ['POST'])
def results():
	print "got post info"
	name = request.form['name']
	email = request.form['email']
	language = request.form['favlanguage']
	comment = request.form['comment']
	print name
	print email
	if len(request.form['name']) < 1:
		flash("Invalid Name")
		return render_template("index.html")
	elif not EMAIL_REGEX.match(request.form['email']):
		flash("Invalid Email")
		return	render_template("index.html")
	elif len(request.form['comment']) < 1:
		flash("Invalid Comment")
		return render_template("index.html")
	elif len(request.form['comment']) >= 120:
		flash("Invalid Comment: Do not excede 120 characters")
		return render_template("index.html")
	return render_template("results.html", name = name, email = email, language = language, comment = comment)
app.run(debug=True)