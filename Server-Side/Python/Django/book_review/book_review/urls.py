"""book_review URL Configuration

The `urlpatterns` list routes URLs to views. For more information please see:
    https://docs.djangoproject.com/en/1.10/topics/http/urls/
Examples:
Function views
    1. Add an import:  from my_app import views
    2. Add a URL to urlpatterns:  url(r'^$', views.home, name='home')
Class-based views
    1. Add an import:  from other_app.views import Home
    2. Add a URL to urlpatterns:  url(r'^$', Home.as_view(), name='home')
Including another URLconf
    1. Import the include() function: from django.conf.urls import url, include
    2. Add a URL to urlpatterns:  url(r'^blog/', include('blog.urls'))
"""
from django.conf.urls import url, include
from django.contrib import admin

urlpatterns = [
    url(r'^admin/', admin.site.urls),
    url(r'^', include('apps.book_review.urls')), 
    url(r'^register', include('apps.book_review.urls')),
    url(r'^books', include('apps.book_review.urls')),
    url(r'^login', include('apps.book_review.urls')),
    url(r'^logout', include('apps.book_review.urls')),
    url(r'^books/add', include('apps.book_review.urls')),
    url(r'^book/add/process', include('apps.book_review.urls')),
    url(r'^books/delete/', include('apps.book_review.urls')),
    url(r'books/', include('apps.book_review.urls')),
    url(r'^add_review', include('apps.book_review.urls')),
    url(r'^review/delete/', include('apps.book_review.urls')),
    url(r'^users/', include('apps.book_review.urls'))

]
