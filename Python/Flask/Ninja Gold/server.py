from flask import Flask, render_template, redirect, request, session

import random

app = Flask(__name__)
app.secret_key = "SuperDuperSecret"

@app.route("/")
def index():
	money = session['money']
	return render_template("index.html", money = money)

@app.route("/process_money", methods = ["POST"])
def process_money():
	which_form = request.form['building']
	if which_form == "farm":
		x = random.randrange(10,21)
		session['money'] = session['money'] + x 
		print "farm"
		print "the amount won = {}".format(x)
		print "the total = {}".format(session['money'])

	elif which_form == "cave":
		x = random.randrange(5,11)
		session['money'] = session['money'] + x
		print "cave"
		print "the amount won = {}".format(x)
		print "the total = {}".format(session['money'])
	elif which_form == "house":
		x = random.randrange(2,6)
		session['money'] = session['money'] + x
		print "house"
		print "the amount won = {}".format(x)
		print "the total = {}".format(session['money'])
	else:
		x = random.randrange(-50,51)
		session['money'] = session['money'] + x
		print "casino"
		print "the amount won = {}".format(x)
		print "the total = {}".format(session['money'])
	return redirect("/")


@app.route("/clear", methods = ["POST"])
def clear():
	session['money'] = 0
	return redirect("/")

app.run(debug=True)