from django.shortcuts import render, redirect, HttpResponse
from django.contrib import messages
from models import *
import bcrypt

def index(request):
    if 'id' in request.session:
        return redirect('/homepage')
    return render(request, 'index.html')

def register(request):
    errors = User.objects.registration_validator(request.POST)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags = tag)
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
        return redirect ('/books')

def books(request):
    if 'id' not in request.session:
        return redirect('/')
    context = {
        'user_name': User.objects.get(id = request.session['id']),
        'books': Book.objects.order_by("-created_at")[:3],
        'all_books': Book.objects.order_by("-created_at"),
        
        }
    return render(request, 'books.html', context)

def login(request):
    errors = User.objects.login_validator(request.POST)
    if len(errors) > 0:
        for tag, error in errors.iteritems():
            messages.error(request, error, extra_tags=tag)
        return redirect('/')
    else:
        request.session['id'] = User.objects.get(email = request.POST['login_email']).id
    return redirect('/books')

def logout(request):
    del request.session['id']
    print "=" *100
    print "LOGGED OUT"
    print "=" *100
    return redirect('/')

def add_book_and_review(request):

    return render(request, "add_book_and_review.html")

def add_book_and_review_process(request):
    #adding to Book table
    user = User.objects.get(id = request.session['id'])
    title = request.POST['title']
    author = request.POST['author']
    book = Book.objects.create(user = user, title = title, author = author)
    print "ADDED BOOK"
    book_id = Book.objects.last().id


    #adding to review table
    user = User.objects.get(id = request.session['id'])
    review = request.POST['review']
    rating = request.POST['rating']
    Review.objects.create(book = book, user = user, review = review, rating = rating)
    print "ADDED REVIEW"

    return redirect("/books/{}".format(book_id))

def book_info(request, number):
    context = {
        'book_info': Book.objects.get(id = number),
        'reviews': Review.objects.filter(book_id = number),
        'user': User.objects.get(id = request.session['id'])
    }
    return render(request, 'book_info.html', context)

def add_review(request):
    user = User.objects.get(id = request.session['id'])
    review = request.POST['review']
    rating = request.POST['rating']
    book = Book.objects.get(id = request.POST['book_id'])
    Review.objects.create( book = book, user = user, review = review, rating = rating )
    print "ADDED ADDITIONAL REVIEW"
    return redirect('/books/{}'.format(request.POST['book_id']))

def delete_book(request, number):
    a = Book.objects.get(id = number)
    a.delete()
    print "=" * 100
    print "DELETED BOOK"
    print "=" *100
    return redirect('/books')

def delete_review(request, number):
    book = Review.objects.get(id = number).book_id
    a = Review.objects.get(id = number)
    a.delete()
    print "=" * 100
    print "DELETED REVIEW"
    print "=" *100
    return redirect('/books/{}'.format(book))

def users(request, number):
    context = {
        'user': User.objects.get(id = number),
        'review_count': Review.objects.filter(user_id = number).count(),
        'all_reviews': Review.objects.filter(user_id = number)
    }
    return render(request, 'users.html', context)
