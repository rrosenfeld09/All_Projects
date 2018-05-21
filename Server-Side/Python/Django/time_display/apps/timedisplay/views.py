from django.shortcuts import render, HttpResponse, redirect
from time import gmtime, strftime


def index(request):
    content = {
        'time': strftime("%b %d, %Y %H:%M %p", gmtime())
    }
    return render(request, "index.html", content)
