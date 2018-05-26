from django.shortcuts import render, redirect, HttpResponse
from models import User, Message
from django.contrib import messages

def index(request):
    if 'id' in request.session:
        return redirect('/dashboard')
    return render(request, 'index.html')

def login(request):
    if 'id' in request.session:
        return redirect('/dashboard')
    return render(request, 'login.html')

def login_process(request):
    errors = User.objects.login_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags = tag)
        return redirect('/login')
    else:
        request.session['id'] = User.objects.get(email = request.POST['login_email']).id
        admin_check = User.objects.get(id = request.session['id']).admin
        print admin_check
        if admin_check == True:
            return redirect('/dashboard/admin')
        else: 
            return redirect('/dashboard')

def register(request):
    if 'id' in request.session:
        return redirect('/dashboard')
    return render(request, 'register.html')

def register_process(request):
    errors = User.objects.registration_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT REGISTER"
        return redirect('/register')
    else:
        print "REGISTERED"
        num_registered = User.objects.all()
        
        first_name = request.POST['first_name']
        last_name = request.POST['last_name']
        email = request.POST['email']
        password = request.POST['password']
        if len(num_registered) == 0:
            admin = True
        else:
            admin = False
        User.objects.create(first_name = first_name, last_name = last_name, email = email, password = password, admin = admin)     
        request.session['id'] = User.objects.get(email = email).id
        print request.session['id']       
        return redirect('/dashboard')

def dashboard(request):
    if 'id' not in request.session:
        return redirect('/')
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        return redirect('/dashboard/admin')
    else:
        context = {
            'users': User.objects.all()
        }
    return render(request, 'dashboard.html', context)

def admin_dashboard(request):
    if 'id' not in request.session:
        return redirect('/')
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        context = {
            'users': User.objects.all()
        }
        return render(request, 'admin_dashboard.html', context)
    else:
        return redirect('/dashboard')

def new_user(request):
    if 'id' not in request.session:
        return redirect('/')
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        return render(request, 'new_user.html')
    else:
        return redirect('/dashboard')

def admin_add(request):
    errors = User.objects.registration_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT REGISTER"
        return redirect('/users/new')    
    else:
        first_name = request.POST['first_name']
        last_name = request.POST['last_name']
        email = request.POST['email']
        password = request.POST['password']
        admin = False
        User.objects.create(first_name = first_name, last_name = last_name, email = email, admin = admin, password = password)
    return redirect('/dashboard/admin')

def show(request, number):
    if 'id' not in request.session:
        return redirect('/')
    else:
        this_user = User.objects.get(id = number)
        
        context = {
            'user': User.objects.get(id = number), 
            'messages': this_user.messages.all(),
            'logged_user': User.objects.get(id = request.session['id'])
        }
        return render(request, 'show.html', context)

def logout(request):
    del request.session['id']
    print 'LOGGED OUT'
    return redirect('/')

def delete(request, number):
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        a = User.objects.get(id = number)
        a.delete()
        return redirect('/dashboard/admin')
    else:
        return redirect('/')

def edit(request, number):
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        context = {
            'user': User.objects.get(id = number)
        }
        return render(request, 'edit.html', context)
    else:
        return redirect('/')
def admin_edit(request, number):
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        user_to_edit = User.objects.get(id = number)
        if len(request.POST['first_name']) > 0:
            user_to_edit.first_name = request.POST['first_name']
        if len(request.POST['last_name']) > 0:
            user_to_edit.last_name = request.POST['last_name']
        if len(request.POST['email']) > 0:
            user_to_edit.email = request.POST['email']
        if request.POST['user_level'] == 'admin':
            user_to_edit.admin = True
        else:
            user_to_edit.admin = False
        user_to_edit.save()
    return redirect('/dashboard/admin')

def admin_password(request, number):
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        user_to_edit = User.objects.get(id = number)
        user_to_edit.password = request.POST['password']
        user_to_edit.save()
    return redirect('/dashboard/admin')
    
def add_message(request, number):
    #VALIDATE
    written_by = User.objects.get(id = request.session['id']).id
    user = User.objects.get(id = number)
    message = request.POST['textarea']
    Message.objects.create(message = message, user = user, written_by = written_by)
    print "ADDED MESSAGE"
    print request.session['id']
    return redirect('/users/show/{}'.format(number))


