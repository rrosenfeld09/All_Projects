# -*- coding: utf-8 -*-
# Generated by Django 1.10 on 2018-05-12 17:46
from __future__ import unicode_literals

from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('users', '0001_initial'),
    ]

    operations = [
        migrations.RenameField(
            model_name='user',
            old_name='full_name',
            new_name='first_name',
        ),
        migrations.AddField(
            model_name='user',
            name='last_name',
            field=models.CharField(default='None', max_length=255),
            preserve_default=False,
        ),
    ]