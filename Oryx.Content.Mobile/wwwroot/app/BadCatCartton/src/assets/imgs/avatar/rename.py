import os
# os.path.abspath('.')
avataIndex = 1
for item in os.listdir('.'):
    extension = os.path.splitext(item)[1]
    if(extension == '.jpeg' or extension == '.jpg'):
        base = os.path.abspath('.')
        src = os.path.join(base, item)
        drcName = 'avatar_'+str(avataIndex)+'.jpg'
        drc = os.path.join(base, drcName)
        print(avataIndex)
        os.rename(src, drc)
    avataIndex += 1