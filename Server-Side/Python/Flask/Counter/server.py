from flask import Flask, render_template, session, redirect

app = Flask(__name__)
app.secret_key = "ThisIsSecret"


@app.route("/")
def index():
	try:
		session['num_views'] += 1
	except KeyError:
		session['num_views'] = 1
	return render_template("index.html", num_views = session['num_views'])

@app.route("/plustwo", methods = ["POST"])
def plus_two():
	try:
		session['num_views'] += 1
	except KeyError:
		session['num_views'] = 1		
	return redirect("/")

@app.route("/reset", methods = ["POST"])
def reset():
	try:
		session['num_views'] = 0
	except KeyError:
		session['num_views'] = 1
	return redirect("/")
	

app.run(debug=True)