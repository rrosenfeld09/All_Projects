from flask import Flask, render_template

app = Flask(__name__)

@app.route('/')
def index():
	return render_template("index.html")

@app.route('/ninja')
def ninjas():
	return render_template("ninja.html")

@app.route('/ninja/<color>')
def ninja_color(color):
	if color in ['blue', 'red', 'orange', 'purple']:
		return render_template(color + ".html")
	else:
		return render_template("else.html")


app.run(debug=True)