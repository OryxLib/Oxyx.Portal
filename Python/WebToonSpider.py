#!/usr/bin/python
# -*- coding: UTF-8 -*-
from selenium import webdriver
from selenium.webdriver.common.by import By
import threading
import time
import urllib
import os


def downloadImg(url, fileName):
    f = urllib.urlopen(url)
    img = open(fileName, 'wb')
    #print('download:'+str(fileName))
    img.write(f.read())


def queryHtml(url):
    driver = webdriver.PhantomJS()
    driver.get(url)
    print(driver.find_element_by_tag_name('body').get_attribute('innerHTML'))


def queryContentDetail(url, path):
    driver = webdriver.PhantomJS()
    driver.get(url)
    targetImgList = driver.find_elements_by_css_selector(".contentNovel img")
    for tarImg in targetImgList:
        print('download tarImg')
        originVal = tarImg.get_attribute("data-original")
        srcVal = tarImg.get_attribute("src")
        imgUrl = originVal if originVal else srcVal
        imgName = imgUrl.split('/')[-1]
        print(imgUrl)
        if not os.path.isfile(dir+'/'+imgName):
            downloadImg(imgUrl, dir+'/'+imgName)

# 获取章节内容


def queryContentList(url):
    drvier = webdriver.PhantomJS()
    drvier.get(url)
    targetContentList = drvier.find_elements_by_css_selector(
        ".chapterList .item a")

    # title
    title = drvier.find_element_by_css_selector(
        '.container .title').get_attribute('innerText')

    path = './'+title
    if not os.path.exists(path):
        os.makedirs(path)

    # cover pic
    coverPic = drvier.find_element_by_css_selector(
        '.coverContent .bg img').get_attribute('src')
    if not os.path.isfile(path+'/coverPic.jpg'):
        downloadImg(coverPic, path+'/coverPic.jpg')

    tagsList = [item.get_attribute(
        'innerText') for item in drvier.find_elements_by_css_selector('.tags .label')]
    # print(len(tagsList))

    for tarContent in targetContentList:
        nextUrl = tarContent.get_attribute('href')
        dirIndex = nextUrl.split('/')[-1]
        if not os.path.exists(path+'/'+dirIndex):
            os.makedirs(path+'/'+dirIndex)
        queryContentDetail(nextUrl, path+'/'+dirIndex)


def queryListByPage(url):
    queryContentThreadList = []
    driver = webdriver.PhantomJS()
    
    driver.get(url)
    targetAList = driver.find_elements_by_css_selector(".rightInfo a")
    for tarEle in targetAList:
        queryContentThreadList.append(
            queryContentThread(tarEle.get_attribute('href')))
        print(tarEle.get_attribute('href'))
    for qtTItem in queryContentThreadList:
        qtTItem.start()
        qtTItem.join()


class queryContentThread(threading.Thread):
    def __init__(self, url):
        threading.Thread.__init__(self)
        self.url = url

    def run(self):
        print('process queryContentThread: '+self.url)
        queryContentList(self.url)


class queryThread(threading.Thread):
    def __init__(self, url):
        threading.Thread.__init__(self)
        self.url = url

    def run(self):
        print('process : '+self.url)
        queryListByPage(self.url)


def startQuery():
    threadList = []
    for queryIndex in range(1, 22):
        threadList.append(queryThread(
            'http://www.weehui.com/cartoon/list/'+str(queryIndex+1)))

    for threadItem in threadList:
        # threadItem.setDaemon(False)
        #print("process index:"+str(queryIndex))
        threadItem.start()
        threadItem.join()


startQuery()
# queryHtml('http://www.weehui.com/cartoon/read/956fa1d015a5cbbace73f628baa8236d/1')
# downloadImg('http://e.hiphotos.baidu.com/image/h%3D300/sign=50fc711ddf1b0ef473e89e5eedc451a1/b151f8198618367a2e8a46ee23738bd4b31ce586.jpg')
