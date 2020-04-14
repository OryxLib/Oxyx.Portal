import numpy
#abs()
#绝对值函数
#Return the absolute value of a number 
#arg: int , float / complex
#return: absolute value  / magnitude
abs(-1) == abs(1)

#all()
#判断集合所有元素为True
#Return True if all iterable is true
all([True,False]) #False
all([True,True]) #True

#any()
#判断任意值为True
#Return True if any iterable item is true
any([True,False]) #True
any([False,False]) #False

#ascii()
#返回字符的ascii码, 2.7中无法使用
#2.7 中使用  ord('a') 进行ascii 码转换
#2.7中可使用 repr 返回16进制编码
#As repr() , return a string containing a printable representation of a object

#repr()
#返回字符编码

#bin()
#二进制转换
#Convert an integer number to a binary string prefixed with "0b"
bin(3) #0b11

#bool([x])
#返回一个布尔值(Boolean)
#x 是一个任意类型, 使用标准规则转换为bool值
#列举为False 的情况(此处类似Javascript类型转换)
bool(0) #False
bool() #False
bool('') #False
bool([]) #False
bool(None) #False
bool(numpy.nan) #False 

#breakpoint(*args,**kws)
#3.7 新增
# 进入调试器 
# 默认情况下 pdb.set_trace()

#bytearray([source[,encoding[,errors]])
#返回一个byte数组
# 1 .如果source 为string 则需要传入encoding
# 2 .如果source 为integer 则数组需要设定大小(size) 以及 使用null 初始化
# 3 .如果是一个实现了buffer 接口的object 对象, 则使用一个只读的buffer 初始化bytearray 并返回
# 4 .如果source 是一个集合, 则source必须是一个integer(数值)型集合, 且集合大小在0 - 256 之间

#bytes 
#返回一个不可变bytearray
#规则同上, 不同的情况是此类型不可变(immutable)

#callable(object) 
#3.2新增
#如果对象(object)允许调用, 则返回True, 否则返回False.
#需注意, 即使object 可被调用, 但其仍然可能会返回False, 
#        如果返回False 则一定不可被调用
#可调用类(callable class) 被调用时返回实例(instance); 换言之, 实例的类型如果包含__call()__方法, 则调用此类型可返回实例
#https://docs.python.org/3/library/functions.html#callable

#chr(integer)
#数字转换为字符, ascii码转换

# @classmethod
# 类方法标记
# 使用此标记后, 方法的第一个参数为class 的实例, 类似语法糖, 实现面向对象操作
# cInstance.f()
# class C:
#    @classmethod
#    def f(cls, arg1, arg2, ...): ...


#compile
#编译源码
#>>> str = "for i in range(0,10): print(i)" 
#>>> c = compile(str,'','exec')   # 编译为字节代码对象 
#>>> c
#<code object <module> at 0x10141e0b0, file "", line 1>
#>>> exec(c)

#complex
#complex() 函数用于创建一个值为 real + imag * j 的复数或者转化一个字符串或数为复数。
# 如果第一个参数为字符串，则不需要指定第二个参数。。

#delattr
#用于删除属性
# delattr(obj,name)

#dict()
#创建一个字典
#dict(a='a', b='b', t='t') 
#dict(zip(['one', 'two', 'three'], [1, 2, 3]))
#dict([('one', 1), ('two', 2), ('three', 3)])  
#不同于js, 字典与object 不同的对象

#divmod(a,b)
#除法, 返回a与b 的除数 与余数一同返回
#2.3之前不允许处理复数
#divmod(7, 2) //(3, 1)
#divmod(8, 2) //(4, 0)

#enumerate([])
#将一个可遍历对象组合为一个索引序列
#for item in []:
#for index,item in enumrate([]):

#eval()
#eval() 函数用来执行一个字符串表达式，并返回表达式的值。
#x = 7
#eval( '3 * x' )

#execfile()
#execfile(filePath) 函数可以用来执行一个文件。

#file(name[, mode[, buffering]])
#file() 函数用于创建一个 file 对象，它有一个别名叫 open()，更形象一些，它们是内置函数。参数是以字符串的形式传递的。
#更多文件操作可参考：Python 文件I/O。

#filter(function, iterable)
#filter() 函数用于过滤序列，过滤掉不符合条件的元素，返回由符合条件元素组成的新列表。

#float()
#float() 函数用于将整数和字符串转换成浮点数。

#format()
#Python2.6 开始，新增了一种格式化字符串的函数 str.format()，它增强了字符串格式化的功能。
#基本语法是通过 {} 和 : 来代替以前的 % 。
# "{1} {0} {1}".format("hello", "world") //'world hello world'

#数字化格式
#3.1415926 {:.2f}   3.14
#3.1415926 {:+.2f}  +3.14
#-1        {:+.2f}  -1.00
#2.71828   {:.0f}   3    //不带小数且 进位
#5         {:0>2d}  05
#5         {:x<4d}  5xxx //补右
#10        {:x<4d}  10xx 
#1000000   {:,}     1,000,000
#0.25      {:.2%}   25.00%
#100000000 {:.2e}   1.00e+09
#13        {:10d}       13    //右对齐
#13        {:<10d}  13        //左对齐
#13        {:^10d}     13     //居中对齐
#11        
#           '{:b}'.format(11) 1011 //二进制
#           '{:d}'.format(11) 11   //十进制
#           '{:o}'.format(11) 13   //八进制
#           '{:x}'.format(11) b    //十六进制
#           '{:#x}'.format(11) 0xb //十六进制
#           '{:#X}'.format(11) 0xB //十六进制

# ^, <, > 分别是居中、左对齐、右对齐，后面带宽度
# : 号后面带填充的字符，只能是一个字符，不指定则默认是用空格填充。
# + 表示在正数前显示 +，负数前显示 -
#  （空格）表示在正数前加空格
# b、d、o、x 分别是二进制、十进制、八进制、十六进制。
# 此外我们可以使用大括号 {} 来转义大括号

#frozenset([iterable])
#创建不可变集合
#如果不提供任何参数，默认会生成空集合。

#getattr(obj,name[,default])
#函数用于返回一个对象属性值
#default -- 默认返回值，如果不提供该参数，在没有对应属性时，将触发 AttributeError。

#globals()
#函数会以字典类型返回当前位置的全部全局变量。
#当前脚本作用域下所有可访问到的全局变量

#hasattr()
#函数用于判断对象是否包含对应的属性。

#hash()
#用于获取取一个对象（字符串或者数值等）的哈希值。
# 函数可以应用于数字、字符串和对象，不能直接应用于 list、set、dictionary。
# hash(str([1,2,3])) //913211691
# hash(str(sorted({'1':1}))) //1764348290

#help()
#用于查看模块的帮助信息
#help(sys)

#hex(x)
#函数用于将10进制整数转换成16进制，以字符串形式表示。

#id()
#id([object])  函数用于获取对象的内存地址。

#input()
#input() 函数接受一个标准输入数据，返回为 string 类型。 
#Python2.x 中 input() 相等于 eval(raw_input(prompt)) ，用来获取控制台的输入。
# 注意：input() 和 raw_input() 这两个函数均能接收 字符串 ，但 raw_input() 直接读取控制台的输入（任何类型的输入它都可以接收）。
# 而对于 input() ，它希望能够读取一个合法的 python 表达式，即你输入字符串的时候必须使用引号将它括起来，否则它会引发一个 SyntaxError 。
# 除非对 input() 有特别需要，否则一般情况下我们都是推荐使用 raw_input() 来与用户交互。
# 注意：python3 里 input() 默认接收到的是 str 类型。

#int(x, base=10)
#函数用于将一个字符串或数字转换为整型。
#base -- 进制数，默认十进制。
#int()               # 不传入参数时，得到结果0
#int('12',16)        # 如果是带参数base的话，12要以字符串的形式进行输入，12 为 16进制

#isinstance
#函数来判断一个对象是否是一个已知的类型，类似 type()。
#isinstance (a,(str,int,list))    # 是元组中的一个返回 True
#对于基本类型来说 classinfo 可以是：
# int，float，bool，complex，str(字符串)，list，dict(字典)，set，tuple

#issubclass
#方法用于判断参数 class 是否是类型参数 classinfo 的子类。
#
# class A:
#     pass
# class B(A):
#     pass
    
# print(issubclass(B,A))    # 返回 True

#iter()
#iter(object[, sentinel])函数用来生成迭代器。
# object -- 支持迭代的集合对象。
# sentinel -- 如果传递了第二个参数，则参数 object 必须是一个可调用的对象（如，函数），
# 此时，iter 创建了一个迭代器对象，每次调用这个迭代器对象的__next__()方法时，都会调用 object。

#len()
#返回对象（字符、列表、元组等）长度或项目个数。


#list()
#方法用于将元组转换为列表。
#list((123, 'xyz', 'zara', 'abc')) // [123, 'xyz', 'zara', 'abc']

#locals()
#函数会以字典类型返回当前位置的全部局部变量。

#long()
#函数将数字或字符串转换为一个长整型。
#class long(x, base=10)

#map()
#会根据提供的函数对指定序列做映射。
#map(function, iterable, ...)
#map(lambda x, y: x + y, [1, 3, 5, 7, 9], [2, 4, 6, 8, 10]) //提供了两个列表，对相同位置的列表数据进行相加

#max()
#方法返回给定参数的最大值，参数可以为序列。

#memoryview()
# 函数返回给定参数的内存查看对象(Momory view)。
# 所谓内存查看对象，是指对支持缓冲区协议的数据进行包装，在不需要复制对象基础上允许Python代码访问。
#Python 2.x
#>>>v = memoryview('abcefg')
#>>> v[1]   'b'
#>>> v[-1]  'g'
#>>> v[1:4]  <memory at 0x77ab28>
#>>> v[1:4].tobytes() 'bce'

#Python 3.x
# >>>v = memoryview(bytearray("abcefg", 'utf-8'))
# >>> print(v[1])   # 98
# >>> print(v[-1])  # 103
# >>> print(v[1:4]) # <memory at 0x10f543a08>
# >>> print(v[1:4].tobytes()) # b'bce'

#min()
#方法返回给定参数的最小值，参数可以为序列。

#next() 
#next(iterator[, default])
#返回迭代器的下一个项目。
#default -- 可选，用于设置在没有下一个元素时返回该默认值，如果不设置，又没有下一个元素则会触发 StopIteration 异常。

#oct()
#函数将一个整数转换成8进制字符串。

#open(filename, mode , buffering)
#函数用于打开一个文件，创建一个 file 对象，相关的方法才可以调用它进行读写。
# mode:
# r开头模式是打开已存在文件, 并不能新建
# r   只读, 指针在文件头
# rb  只读二进制, 指针在文件头
# r+  可读写, 指针在文件头头
# rb+ 可读写二进制, 指针在文件头

# w   只写, 打开/新建, 指针在文件头, 会覆盖原有内容
# wb  只写二进制,  打开/新建, 指针在文件头, 会覆盖原有内容
# w+  读写, 打开/新建, 指针在文件头, 会覆盖原有内容
# wb+ 读写二进制, 指针在文件头,  会覆盖原有内容

# a   追加
# ab  二进制追加
# a+  可读写追加
# ab+ 可读写二进制追加

#file 对象方法
#file.read([size]) size未指定则返回整个文件,如果文件大小>2倍内存则有问题.f.read()读到文件尾时返回""(空字串)
#file.readline() 返回一行
#file.readlines([size]) 返回包含size行的列表,size 未指定则返回全部行
#for line in f: print line #通过迭代器访问
#f.write("hello\n") #如果要写入字符串以外的数据,先将他转换为字符串.
#f.tell() 返回一个整数,表示当前文件指针的位置(就是到文件头的比特数).
#f.seek(偏移量,[起始位置]) 用来移动文件指针.
#   偏移量:单位:比特,可正可负
#   起始位置:0-文件头,默认值;1-当前位置;2-文件尾
#f.close() 关闭文件

#ord()
#ord() 函数是 chr() 函数（对于8位的ASCII字符串）或 unichr() 函数（对于Unicode对象）的配对函数，它以一个字符（长度为1的字符串）作为参数，
# 返回对应的 ASCII 数值，或者 Unicode 数值，如果所给的 Unicode 字符超出了你的 Python 定义范围，则会引发一个 TypeError 的异常。

#pow() 
#方法返回 xy（x的y次方） 的值。
#pow(x,y) 等价于 x**y:
#pow(x,y,z) 等价于 x**y%z:

#property()
#函数的作用是在新式类中返回属性值。
#class property([fget[, fset[, fdel[, doc]]]])
#fget 取值器
#fset 赋值器
#fdel 删除器
#class C(object):
    # def __init__(self):
    #     self._x = None
 
    # @property
    # def x(self):
    #     """I'm the 'x' property."""
    #     return self._x
 
    # @x.setter
    # def x(self, value):
    #     self._x = value
 
    # @x.deleter
    # def x(self):
    #     del self._x

#range()
#range(start, stop[, step])
#start: 计数从 start 开始。默认是从 0 开始。例如range（5）等价于range（0， 5）;
#stop: 计数到 stop 结束，但不包括 stop。例如：range（0， 5） 是[0, 1, 2, 3, 4]没有5
#step：步长，默认为1。例如：range（0， 5） 等价于 range(0, 5, 1)

#raw_input() 
#用来获取控制台的输入。

#reduce()
#reduce(function, iterable[, initializer]) 累加器
#在 Python3 中，reduce() 函数已经被从全局名字空间里移除了，它现在被放置在 fucntools 模块里，
# 如果想要使用它，则需要通过引入 functools 模块来调用 reduce() 函数：
#from functools import reduce

#repr()
#返回对象的 展示内容, 类似 C#中自定义ToString()内容

#reversed()
#返回一个反转的迭代器

#round()
#四舍五入

#set([iterable])
#返回集

#setattr(obj, name, value)
#给对象的某个属相赋值

#slice(start, stop[, step])
#切片器
#[2,3,4,5,6][1:3] //[3,4]
#from itertools import islice
#itertools.islice(iterable, start, stop[, step])
#islice([],start,None)

#sorted([],*,key=None,reverse=False)
#返回一个排序的遍历器

#@staticmethod
#定义类内部的静态方法, 即不需要继承self的方法

#str(object='')
#str(object=b'', encoding='utf-8', errors='strict')
#将对象字符串化

#sum([])
#计算集合的和

#super()
#访问父类的构造函数

#tuple([iterable])
#元组

#type(object)
#type(name, bases, dict)
#查看对象的类型

#vars([object])
#查看对象的属性

#zip([],[])
#返回一个元组, 映射集合
#zip([1,2,3],[4,5]) //[(1, 4), (2, 5)]

#__import__ (name, globals=None, locals=None, fromlist=(), level=0)
#动态引用模块

