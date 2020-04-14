<style></style>
<template>
    <div>
        <p>{{title}}</p>
   <quill-editor v-model="content"
                ref="myQuillEditor"
                :options="editorOption"
                @blur="onEditorBlur($event)"
                @focus="onEditorFocus($event)"
                @ready="onEditorReady($event)">
  </quill-editor>
    </div> 
   
</template>

<script>  
    export default {
        name: "Commodities",
        data () {
            return { 
                editorOption: {
                    modules: {
                        toolbar: {
                            container: [
                                ["bold", "italic"],
                                ['image','link']
                            ],
                            handlers: {
                                'link': function(value) {
                                    if (value) {
                                        var href = prompt('Enter the URL');
                                        this.quill.format('link', href);
                                    } else {
                                        this.quill.format('link', false);
                                    }
                                },
                                //handlers object will be merged with default handlers object
                                'image':function(value){
                                   let fileInput = this.container.querySelector('input.ql-image[type=file]');
                                    if (fileInput == null) {
                                        fileInput = document.createElement('input');
                                        fileInput.setAttribute('type', 'file');
                                        fileInput.setAttribute('accept', 'image/png, image/gif, image/jpeg, image/bmp, image/x-icon');
                                        fileInput.classList.add('ql-image');
                                        fileInput.addEventListener('change', () => {
                                        if (fileInput.files != null && fileInput.files[0] != null) {
                                            let reader = new FileReader();
                                            reader.onload = (e) => {
                                            let range = this.quill.getSelection(true);
                                            // this.quill.updateContents(new Delta()
                                            //     .retain(range.index)
                                            //     .delete(range.length)
                                            //     .insert({ image: e.target.result })
                                            // , Emitter.sources.USER);
                                            // var range = this.quill.getSelection();
                                            this.quill.insertEmbed(range.index, 'image', `http://a.hiphotos.baidu.com/image/h%3D300/sign=12c4d521a80f4bfb93d09854334e788f/10dfa9ec8a136327f216788d9d8fa0ec09fac791.jpg`);
                                            fileInput.value = "";
                                            }
                                            reader.readAsDataURL(fileInput.files[0]);
                                        }
                                        });
                                        this.container.appendChild(fileInput);
                                    }
                                    fileInput.click();
                                }
                            }
                        }
                    }, 
                    imageHandler: function(e,v){
                        console.log(11111)
                    }
                },
                title:"新增商品",
                currentInfo:{
                    name:"",
                    cover:"",//封面
                    sellingPrice:9.5,//标价
                    discountedPrice:9.9,//折扣价
                    originalPrice:10.1,//原价
                    detail:""//详情
                },
                content: '',
                // set image upload api url
                path: '/api/v1/help/upload/wangEditorH5File'
            }
        },  
        mounted() {
           
        },
        methods: {
            onEditorBlur(quill) {
                console.log('editor blur!', quill)
            },
            onEditorFocus(quill) {
                console.log('editor focus!', quill)
            },
            onEditorReady(quill) {
                console.log('editor ready!', quill)
            },
            onEditorChange({ quill, html, text }) {
                console.log('editor change!', quill, html, text)
                this.content = html
            },
            setView() {
                
            },
            submit() {
                console.log(this.result)
            }
        },
         computed: {
            editor() {
                return this.$refs.myQuillEditor.quill
            }
        }
    }
</script>
