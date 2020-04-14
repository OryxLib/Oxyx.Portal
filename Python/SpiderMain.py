#!/usr/bin/python
# -*- coding: UTF-8 -*-
import SpiderExcutor
from selenium import webdriver
from selenium.webdriver.common.by import By
#import urllib  # py2.7
from urllib import request #py3.6
import random
import os

# 单线程 异步 任务执行器
excutor = SpiderExcutor.TaskExcutor()

# test excutor
# def goFun2(param):
#     param1, param2 = param
#     print('++'+str(param1)+"+++"+str(param2))


# def goFun(param):
#     print(param)
#     for item2 in range(3):
#         excutor.TaskEnqueue(goFun2, (param, item2))


# for item in range(10):
#     excutor.TaskEnqueue(goFun, item)

# tool : get proxy from file
def getProxy():
    with open('./proxyList.txt', 'r') as proxyFile:
        proxyList = proxyFile.readlines()
        proxyArr = random.choice(proxyList).split(':')
        return (proxyArr[0]+":"+proxyArr[1], 'http')

# tool : download img


def downloadImg(url, fileName):
    f = request.urlopen(url)
    img = open(fileName, 'wb')
    # print('download:'+str(fileName))
    img.write(f.read())

# tool get phantomjs driver


def getDriver():
    sessionKey = "wSession"
    sessionValue = "00E85263AC13FF6F99BF9D3942A1E130E88FC963"
    sessionDomain = ".weehui.com"
    sessionPath = "/"
    proxyIp, proxyPort = getProxy()
    print(proxyIp+"_"+proxyPort)
    service_args = [
        '--proxy='+proxyIp,
        '--proxy-type='+proxyPort,
    ]
    driver = webdriver.PhantomJS(service_args=service_args)
    driver.add_cookie({
        'domain': sessionDomain,  # note the dot at the beginning
        'name': sessionKey,
        'value': sessionValue,
        'path': sessionPath,
        'expires': None
    })
    return driver


def queryThirdLevel(param):
    thrUrl, dirPath = param
    driver = getDriver()
    driver.get(thrUrl)
    targetImgList = driver.find_elements_by_css_selector(".contentNovel img")
    for tarImg in targetImgList:
        downloadImg(tarImg.get_attribute("data-original"),
                    dirPath+'/'+tarImg.get_attribute("data-original").split('/')[-1])


def querySecondLevel(secUrl):
    print('sec url '+secUrl)
    driver = getDriver()
    driver.get(secUrl)
    # 目标元素
    targetContentList = driver.find_elements_by_css_selector(
        ".chapterList .item a")

    title = driver.find_element_by_css_selector(
        '.container .title').get_attribute('innerText')
    path = './'+title
    print('get page title:'+title)
    if not os.path.exists(path):
        os.makedirs(path)

    # cover pic
    coverPic = driver.find_element_by_css_selector(
        '.coverContent .bg img').get_attribute('src')
    if not os.path.isfile(path+'/coverPic.jpg'):
        downloadImg(coverPic, path+'/coverPic.jpg')

    # 目标章节目录名list
    # tagsList = [item.get_attribute(
    #     'innerText') for item in driver.find_elements_by_css_selector('.tags .label')]

    for tarContent in targetContentList:
        try:
            nextUrl = tarContent.get_attribute('href')
            dirIndex = nextUrl.split('/')[-1]
            subDir = path+'/'+dirIndex
            if not os.path.exists(subDir):
                os.makedirs(subDir)
            excutor.TaskEnqueue(queryThirdLevel, (nextUrl, subDir)) 
        except TypeError:
             print('Type Error')


def queryFirstLevel(pageIndex):
    driver = getDriver()
    driver.get('http://www.weehui.com/cartoon/list/'+str(pageIndex))
    print('get page cartoon : '+str(pageIndex))
    print(driver.find_element_by_css_selector(
        'body').get_attribute('innerHTML'))
    targetAList = driver.find_elements_by_css_selector(".rightInfo a")
    for targEle in targetAList:
        secondUrl = targEle.get_attribute('href')
        excutor.TaskEnqueue(querySecondLevel, secondUrl)

# query content


def queryStart():
    for pageIndex in range(1, 2):
        excutor.TaskEnqueue(queryFirstLevel, pageIndex)


queryStart()
