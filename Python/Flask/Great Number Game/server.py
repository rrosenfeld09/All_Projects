from flask import Flask, render_template, session, redirect, request

import random

app = Flask(__name__)
app.secret_key = "SuperSecret"

@app.route("/")
def index():
	try: 
		if session['gameover'] == True:
			session['random'] = random.randrange(1, 101)
	except NameError:
		x = random.randrange(1,101)
		session['random'] = x
		session['gameover'] = True
	print session['random']
	print session['answer']
	return render_template("index.html")

@app.route("/answer", methods = ["POST"])
def answer():
	session['gameover'] = False
	session['answer'] = int(request.form['answer'])
	return redirect("/")

@app.route("/reset", methods = ["POST"])
def reset():
	x = random.randrange(1,101)
	session['random'] = x
	session['answer'] = -1
	session['gameover'] = True
	return redirect("/")



app.run(debug=True)