from django.shortcuts import render, redirect, HttpResponse


def index(request):
    if 'attempts' not in request.session:
        request.session['attempts'] = 1
    else:
        request.session['attempts'] += 1
    
    print "====", request.session['attempts'], "===="
    return render(request, "index.html")



