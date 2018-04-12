from flask import Flask, render_template, session, redirect, request


import random

app = Flask(__name__)
app.secret_key = "SuperSecret"

@app.route("/")
def index():
	x = random.randrange(0,101)
	session['random'] = x
	print session['random']
	return render_template("index.html")

@app.route('/answer', methods = ["POST"])
def answer():
	session['answer'] = request.form['answer']
	print session['answer']
	return redirect('/')



app.run(debug=True)