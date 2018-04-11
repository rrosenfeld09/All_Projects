from flask import Flask, render_template, request, redirect

app = Flask(__name__)

@app.route('/')
def index():
	return render_template("index.html")

@app.route('/results', methods = ['POST'])
def results():
	print "got post info"
	name = request.form['name']
	email = request.form['email']
	language = request.form['favlanguage']
	print name
	print email
	return render_template("results.html", name = name, email = email, language = language)
app.run(debug=True)