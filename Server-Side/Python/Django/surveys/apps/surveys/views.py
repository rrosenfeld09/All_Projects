from django.shortcuts import render, HttpResponse, redirect


def index(request):
    return render(request, "index.html")

def process(request):

    if 'submissions' not in request.session:
        request.session['submissions'] = 1
    else:
        request.session['submissions'] += 1
    request.session['context'] = {
    'name': request.POST['name'],
    'location': request.POST['location'],
    'language': request.POST['language'],
    'comment': request.POST['textarea']
    }
    print "=" *100
    return redirect('/results')

def results(request):
    print "im at the results page now"
    print request.session['submissions']
    context = request.session['context']
    return render(request, "results.html", context)

def go_back(request):
    return redirect('/')

def reset(request):
    print "=====SESSION CLEARED====="
    del request.session['submissions']
    return redirect('/')


