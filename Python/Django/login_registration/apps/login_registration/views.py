


from django.shortcuts import render, redirect, HttpResponse
from django.contrib import messages
from models import *
import bcrypt


def index(request):
    return render(request, 'index.html')

def register(request):
    #checking for any errors in validations
    errors = User.objects.registration_validator(request.POST)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "=" * 100
        print "COULD NOT REGISTER"
        print "=" * 100
        return redirect('/')
    else:
        #inserting POST data into database
        first_name = request.POST['first_name']
        last_name = request.POST['last_name']
        email = request.POST['email']
        password = bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt())
        User.objects.create(first_name = first_name, last_name = last_name, email = email, password = password)
        print "=" * 100
        print "REGISTERED"
        print "=" * 100
        request.session['id'] = User.objects.get(email = email).id
        return redirect ('/success')

def success(request):
    context = {
        'user_name': User.objects.get(id = request.session['id'])
    }
    return render(request, 'success.html', context)

def login(request):
    errors = User.objects.login_validator(request.POST)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        return redirect('/')
    else:
        request.session['id'] = User.objects.get(email = request.POST['login_email']).id
    return redirect('/success')

def logout(request):
    del request.session['id']
    print "=" *100
    print "LOGGED OUT"
    print "=" *100
    return redirect('/')
