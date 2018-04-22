from flask import Flask, render_template, session, redirect, request

import random

app = Flask(__name__)
app.secret_key = "SuperSecret"

@app.route("/")
def index():
	dump_data = False
	if dump_data:
		session['random'] = None
		session['gameover'] = None
		session['answer'] = None

	if session.get('random') == None:
		print("NEW SESSION!")
	else:
		print("RESUMING SESSION")

	if session.get('gameover') == None:
		print("GAMEOVER IS ALSO NONE")
	else:
		print("GAMEOVER HAS A VALUE")
		
	try: 
		if session.get('gameover') == None:
			session['gameover'] = True
		else:
			print session['gameover']

		if session['gameover'] == True:
			print("GAME IS OVER, GENERATING NEW RANDOM NUMBER")
			session['random'] = random.randrange(1, 101)
			session['answer'] = -1
		else:
			print("ELSE STATEMENT")
			print session['gameover']
		print("BEGIN GAME")
	except NameError:
		x = random.randrange(1,101)
		session['random'] = x
		session['gameover'] = True
	print session['random']
	print session['answer']
	return render_template("index.html")

@app.route("/answer", methods = ["POST"])
def answer():
	if session.get('gameover') == None:
		print("IGNORING SUBMISSION FOR NONEXISTANT SESSION")
	else:
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
