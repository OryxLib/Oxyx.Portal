<style></style>
<template>
    <div>
        <p>{{title}}</p>
       <Button @click="addCoustomer">新增用户</Button>
            <Table border :columns="columns" :data="list" ></Table>
    </div> 
</template>

<script>
    export default {
        name: "CustomerList",
        data () {
            return {
                title:"CustomerList",
                columns:[
                    {
                        title:"用户名",
                        key:"Name"
                    },
                    {
                        title:"联系方式",
                        key:"PhoneNumber"
                    },
                    {
                        title:"地址",
                        key:"Address"
                    },
                    {
                        title:"注册时间",
                        key:"CreateTime"
                    },
                    {
                        title:"操作",
                        key:"action",
                        render:(h, params) => {
                            return h('div',[
                                h('Button',{
                                    props: {
                                        type: 'primary',
                                        size: 'small'
                                    },
                                    on:{
                                        click: () => {
                                        console.log("route to "+params.row.AccounId);
                                        }
                                    }
                                },"编辑"),
                                h('Button',{
                                    props: {
                                        type: 'error',
                                        size: 'small'
                                    },
                                    on:{
                                    click: () => {
                                        console.log("delete account where id ="+params.row.AccounId);
                                        }
                                    }
                                },"删除"),
                            ]
                            )

                        }
                    }
                ],
                list:[
                    {
                        AccounId:"111111",
                        Name:"yuhui1",
                        PhoneNumber:"176*****993",
                        Address:"北京海淀区",
                        CreateTime:"2018/01/10 12:01:22" 
                    },
                    {
                        AccounId:"22222",
                        Name:"jery",
                        PhoneNumber:"176*****993",
                        Address:"shanghai",
                        CreateTime:"2018/01/10 12:01:22" 
                    },
                    {
                        AccounId:"33333",
                        Name:"Tom",
                        PhoneNumber:"176*****993",
                        Address:"shanghai",
                        CreateTime:"2018/01/10 12:01:22" 
                    }
                ]
            }
        },
        methods: {
            addCoustomer:function(){
                var self=this;
                //跳转
            }
        }
    }
</script>
