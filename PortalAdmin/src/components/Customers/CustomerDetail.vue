<style></style>
<template>
    <div>
        <p>{{title}}</p>
        <Form :model="formItem" :label-width="80">
            <FormItem label="用户名">
                <Input v-model="info.Name" placeholder="输入用户名称"></Input>
            </FormItem>
            <FormItem label="电话">
                <Input v-model="info.PhoneNumber" placeholder="输入用户电话"></Input>
            </FormItem>
            <FormItem label="地址">
                <Input v-model="info.Address" placeholder="输入用户地址"></Input>
            </FormItem>
            <FormItem>
                <Button type="primary" @click="submit">提交</Button>
                <Button type="ghost" style="margin-left: 8px" @click="reset">重置</Button>
            </FormItem>
        </Form>
    </div> 
</template>

<script>
    export default {
        name: "CustomerDetail",
        data () {
            return {
                title:"CustomerDetail",
                info:{
                    // UserCode:"",
                    // OpenId:"",
                    // NickName:"",
                    AccounId:"",
                    Name:"yuhui",
                    PhoneNumber:"176*****993",
                    Address:"北京海淀区",
                    CreateTime:"" 
                },
                origin:{}
            }
        },
        methods: {
            init: function(id) {
                var self=this;
                var info={};
                if(id){
                    //ajax get customer info
                }
                else{
                    info={
                        Name:"yuhui",
                        PhoneNumber:"176*****993",
                        Address:"北京海淀区",
                        CreateTime:"" 
                    };
                }
                self.info=info;
                self.origin=info;
            },
            submin: function(){
                var self=this;
                //todo 提交修改信息
                console.log(self.info);
            },
            reset:function(){
                var self=this;
                self.info=self.origin;
            }
        }
    }
</script>


