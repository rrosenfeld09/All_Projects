from django.shortcuts import render, redirect, HttpResponse
import random
from time import gmtime, strftime

def index(request):
    if 'money' not in request.session:
        request.session['money'] = 0
    print "total money =",request.session['money']
    return render(request, 'index.html')

def process_money(request):
    which_form = request.POST['building']

    #LOGIC TO PRODUCE SESSION DATA TO LOG ACTIVITIES
    if 'activities' not in request.session:
        request.session['activities'] = []

    request.session['status'] = 0

    print "=" *100

 
    #LOGIC TO DETERMINE THE TOTAL AMOUNT OF MONEY AFTER USER SUBMITS FORM DATA
    if which_form == 'farm':
        x = random.randrange(10,21)
        request.session['money'] += x
        # print request.session['money']
        print "amount won:", x
        statement = "Earned {} golds from the {} ({})".format(x, which_form, strftime("%b %d, %Y %H:%M %p", gmtime()))
        request.session['activities'].insert(0,statement)
    elif which_form == 'cave':
        x = random.randrange(5,11)
        request.session['money'] += x
        # print request.session['money']
        print "amount won:", x
        statement = "Earned {} golds from the {} ({})".format(x, which_form, strftime("%b %d, %Y %H:%M %p", gmtime()))
        request.session['activities'].insert(0,statement)
    elif which_form == 'house':
        x = random.randrange(2,6)
        request.session['money'] += x
        # print request.session['money']
        print "amount won:", x
        statement = "Earned {} golds from the {} ({})".format(x, which_form, strftime("%b %d, %Y %H:%M %p", gmtime()))
        request.session['activities'].insert(0,statement)
    elif which_form == 'casino':
        x = random.randrange(-50,51)
        request.session['money'] += x
        # print request.session['money']
        print "amount won:", x
        if x >= 0:      
            statement = "Earned {} golds from the {} ({})".format(x, which_form, strftime("%b %d, %Y %H:%M %p", gmtime()))
        else:
            statement = "Ouch.... {} golds from the {}.. ({})".format(x, which_form, strftime("%b %d, %Y %H:%M %p", gmtime()))
            request.session['status'] = 1

        request.session['activities'].insert(0,statement)
    print "new total:", request.session['money']
    print request.session['activities']
    print "=" *100

    return redirect('/')



def clear(request):
    request.session['money'] =0
    request.session['activities'] = []
    print "+++++CLEARED+++++"
    return redirect('/')

