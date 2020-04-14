<style></style>
<template>
    <div>
        <p>{{title}}</p>
    </div> 
</template>

<script>
    export default {
        name: "Commodity",
        data () {
            return {
                title:"新增商品",
                currentInfo:{
                    name:"",
                    cover:"",//封面
                    sellingPrice:9.5,//标价
                    discountedPrice:9.9,//折扣价 
                    originalPrice:10.1,//原价
                    detail:""//详情
                },
                 content: 'base on wangeditor',
                // output content from editor
                result: '',
                // set image upload api url
                path: '/api/v1/help/upload/wangEditorH5File'
            }
        },
        methods: {
            setView() {
                
            },
            submit() {
                console.log(this.result)
            }

        }

    }
</script>
