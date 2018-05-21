from django.shortcuts import render, redirect, HttpResponse

def index(request):
    return render(request, "index.html")

def add_to_cart(request):
    quantity = float(request.POST['quantity'])
    print '=' *100
    print "product id:",request.POST['product_id']
    if request.POST['product_id'] == '1015':
        print "got product 1015"
        request.session['price'] = (19.99 * quantity)
        print "price:", request.session['price']
        print "quantity:", request.POST['quantity']

    elif request.POST['product_id'] == '1016':
        print "got product 1016"
        request.session['price'] = (4.99 * quantity)
        print "price:",request.session['price']
        print "quantity:", request.POST['quantity']

    elif request.POST['product_id'] == '1017':
        print "got product 1017"
        request.session['price'] = (52.99 * quantity)
        print "price:",request.session['price']
        print "quantity:", request.POST['quantity']

    elif request.POST['product_id'] == '1018':
        print "got product 1018"
        request.session['price'] = (249.99 * quantity)
        print "price:",request.session['price']
        print "quantity:", request.POST['quantity']


    if 'total' not in request.session:
        request.session['total'] = request.session['price']
    else:
        request.session['total'] += request.session['price']
    print "total:",request.session['total']
    print '='*100


    return redirect("/")

def checkout(request):
    return render(request, "checkout.html")

def clear(request):
    request.session['total'] =0
    print "+" *100
    print "CART EMPTIED"
    print "total: $0"
    print "+" *100
    return redirect("/")

def go_back(request):
    return redirect("/")