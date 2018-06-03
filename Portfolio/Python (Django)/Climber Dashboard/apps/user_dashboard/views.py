from django.shortcuts import render, redirect, HttpResponse
from models import User, Message, Comment
from django.contrib import messages
import bcrypt

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
def invitation_code(request):
    if request.POST['invitation_code'] == '1234':
        request.session['invitation_code'] = True
        return redirect('/register')
    else:
        return redirect('/')

def register(request):
    if 'id' in request.session:
        return redirect('/dashboard')
    if 'invitation_code' not in request.session:
        return redirect('/')
    return render(request, 'register.html')

def register_process(request):
    errors = User.objects.registration_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT REGISTER"
        return redirect('/register')
    else:
        num_registered = User.objects.all()
        
        first_name = request.POST['first_name']
        last_name = request.POST['last_name']
        email = request.POST['email']
        password = bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt())
        if len(num_registered) == 0:
            admin = True
        else:
            admin = False
        User.objects.create(first_name = first_name, last_name = last_name, email = email, password = password, admin = admin)     
        request.session['id'] = User.objects.get(email = email).id
        print "REGISTERED"
        del request.session['invitation_code']
        return redirect('/dashboard')

def dashboard(request):
    if 'id' not in request.session:
        return redirect('/')
    admin_check = User.objects.get(id = request.session['id']).admin
    if admin_check == True:
        return redirect('/dashboard/admin')
    else:
        context = {
            'this_user': User.objects.get(id = request.session['id']),
            'users': User.objects.all(),
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
        password = bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt())        
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
            'all_messages': this_user.messages.order_by("-created_at"),
            'logged_user': User.objects.get(id = request.session['id']),
            'comments': Comment.objects.all()
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
        user_to_edit.password = bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt())
        user_to_edit.save()
    return redirect('/dashboard/admin')
    
def add_message(request, number):
    errors = Message.objects.message_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT ADD MESSAGE"
        return redirect('/users/show/{}'.format(number))
    else:   
        written_by = User.objects.get(id = request.session['id'])
        user = User.objects.get(id = number)
        message = request.POST['textarea']
        Message.objects.create(message = message, user = user, written_by = written_by)
        print "ADDED MESSAGE"
        print request.session['id']
        return redirect('/users/show/{}'.format(number))

def add_comment(request, number):
    profile = request.POST['profile']
    errors = Comment.objects.comment_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT ADD COMMENT"
        return redirect('/users/show/{}'.format(profile))
    else:
        comment = request.POST['comment']
        user = User.objects.get(id = request.session['id'])
        message = Message.objects.get(id = number)
        Comment.objects.create(comment = comment, user = user, message = message)
        print 'ADDED COMMENT'
        return redirect('/users/show/{}'.format(profile))

def remove_message(request, number):
    this_user = User.objects.get(id = request.session['id']).id
    this_message = Message.objects.get(id = number).written_by.id
    this_profile = Message.objects.get(id = number).user_id
    if this_user == this_message:
        a = Message.objects.get(id = number)
        a.delete()
        return redirect('/users/show/{}'.format(this_profile))
    else:
        return redirect('/')

def remove_comment(request, number):
    this_user = User.objects.get(id = request.session['id']).id
    this_comment = Comment.objects.get(id = number).user_id
    this_message_id = Comment.objects.get(id = number).message_id
    this_profile = Message.objects.get(id = this_message_id).user_id
    if this_comment == this_user:
        a = Comment.objects.get(id = number)
        a.delete()
        return redirect('/users/show/{}'.format(this_profile))
    else:
        return redirect('/')
    
def user_edit(request, number):
    edit_profile = User.objects.get(id = number)
    logged_user = User.objects.get(id = request.session['id'])
    if edit_profile == logged_user:
        return render(request, 'user_edit.html')
    else: 
        return redirect('/dashboard')

def self_edit_info(request, number):
    errors = User.objects.info_update_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT UPDATE EMAIL"
        return redirect('/users/edit_self/{}'.format(number))
    else:
        user_to_edit = User.objects.get(id = number)
        print user_to_edit
        if len(request.POST['email']) > 0:
            user_to_edit.email = request.POST['email']
            print "EDITED EMAIL"
        if len(request.POST['first_name']) > 0:
            user_to_edit.first_name = request.POST['first_name']
            print "EDITED FIRST NAME"
        if len(request.POST['last_name']) > 0:
            user_to_edit.last_name = request.POST['last_name']
            print "EDITED LAST NAME"
        user_to_edit.save()
        return redirect('/dashboard')

def self_edit_pw(request, number):
    errors = User.objects.pw_update_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT UPDATE PASSWORD"
        return redirect('/users/edit_self/{}'.format(number))
    else:
        user_to_edit = User.objects.get(id = number)
        if len(request.POST['password']) > 0 and len(request.POST['confirm_pw']) > 0:
            user_to_edit.password = bcrypt.hashpw(request.POST['password'].encode(), bcrypt.gensalt())
            user_to_edit.save()
        return redirect('/dashboard')

def self_edit_desc(request, number):
    errors = User.objects.desc_update_validator(request)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        print "DID NOT UPDATE EMAIL"
        return redirect('/users/edit_self/{}'.format(number))
    else:
        user_to_edit = User.objects.get(id = number)
        if len(request.POST['desc']) > 0:
            user_to_edit.desc = request.POST['desc']
            user_to_edit.save()
        return redirect('/dashboard')



