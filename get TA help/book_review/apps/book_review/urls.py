from django.conf.urls import url
from . import views

urlpatterns = [
    url(r'^$', views.index),
    url(r'^register$', views.register),
    url(r'^books$', views.books),
    url(r'^login$', views.login),
    url(r'^logout$', views.logout),
    url(r'^books/add$', views.add_book_and_review),
    url(r'^book/add/process$', views.add_book_and_review_process),
    url(r'^books/(?P<number>\d+)$', views.book_info),
    url(r'add_review$', views.add_review),
    url(r'^books/delete/(?P<number>\d+)$', views.delete_book),
    url(r'^review/delete/(?P<number>\d+)$', views.delete_review)
]