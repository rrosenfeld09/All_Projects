from django.shortcuts import render, redirect, HttpResponse

from models import *

def index(request):
    context = {
        'users': User.objects.all()
    }
    return render(request, "index.html", context)

def new(request):
    return render(request, "new.html")

def create(request):
    User.objects.create(first_name = request.POST['first_name'], last_name = request.POST['last_name'], email = request.POST['email'])
    print "="*100
    print "CREATED"
    print "="*100
    return redirect("/users")

def show(request, number):
    context = {
        'user': User.objects.get(id = number)
    }
    return render(request, "show.html", context)

def destroy(request, number):
    User.objects.get(id = number).delete()
    print "="*100
    print "DELETED"
    print "="*100
    return redirect("/users")

def edit(request, number):
    context = {
        'user': User.objects.get(id = number)
    }

    return render(request, "edit.html", context)

def update(request, number):
    u = User.objects.get(id = number)
    u.first_name = request.POST['first_name']
    u.last_name = request.POST['last_name']
    u.email = request.POST['email']
    u.save()
    print "="*100
    print "UPDATED"
    print "="*100
    return redirect('/users/{}'.format(number))