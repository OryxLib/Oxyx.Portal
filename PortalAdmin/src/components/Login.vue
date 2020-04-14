<template>
    <Row id="login-panel">
        <Col span="22" offset="1">
            <Form ref="formRegister" :model="formRegister" :rules="ruleRegister" :label-width="80" v-show="pageStatus">
                <FormItem label="用户名" prop="username">
                    <Input type="text" v-model="formRegister.username"></Input>
                </FormItem>
                <FormItem label="密码" prop="passwd">
                    <Input type="password" v-model="formRegister.passwd"></Input>
                </FormItem>
                <FormItem label="确认密码" prop="passwdCheck">
                    <Input type="password" v-model="formRegister.passwdCheck"></Input>
                </FormItem>
                <FormItem>
                    <Button type="success" @click="handleSubmit('formRegister')">注册</Button>
                </FormItem>
                <FormItem>
                    <a @click="turnStatus">已有账号，去登陆</a>
                </FormItem>
            </Form>

             <Form ref="formLogin" :model="formLogin" :rules="roleLogin" :label-width="80" v-show="!pageStatus">
                <FormItem label="用户名" prop="username">
                    <Input type="text" v-model="formLogin.username"></Input>
                </FormItem>
                <FormItem label="密码" prop="passwd">
                    <Input type="password" v-model="formLogin.passwd"></Input>
                </FormItem>
                <FormItem>
                    <Button type="primary" @click="handleSubmit('formLogin')">登录</Button>
                </FormItem>
                <FormItem>
                    <a @click="turnStatus">没有账号，注册一个</a>
                </FormItem>
            </Form>

        </Col>
    </Row>
</template>
<script>
    export default {
        name: "Login",
        data () {
            const validatePass = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('Please enter your password'));
                } else {
                    if (this.formRegister.passwdCheck !== '') {
                        // 对第二个密码框单独验证
                        this.$refs.formRegister.validateField('passwdCheck');
                    }
                    callback();
                }
            };
            const validatePassCheck = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('Please enter your password again'));
                } else if (value !== this.formRegister.passwd) {
                    callback(new Error('The two input passwords do not match!'));
                } else {
                    callback();
                }
            };
            const validateName = (rule, value, callback) => {
                if (value==='') {
                    return callback(new Error('Name cannot be empty'));
                }else{
                    callback();
                }
            };
            
            return {
                pageStatus:true,
                formRegister: {
                    passwd: '',
                    passwdCheck: '',
                    username: ''
                },
                formLogin: {
                    username:'',
                    passwd:''
                },
                ruleRegister: {
                    passwd: [
                        { validator: validatePass, trigger: 'blur' }
                    ],
                    passwdCheck: [
                        { validator: validatePassCheck, trigger: 'blur' }
                    ],
                    username: [
                        { validator: validateName, trigger: 'blur' }
                    ]
                },
                roleLogin: {
                    username: [
                        { validator: validateName, trigger: 'blur' }
                    ],
                    passwd: []
                }
            }
        },
        methods: {
            handleSubmit (name) {
                this.$refs[name].validate((valid) => {
                    if (valid) {
                        this.$Message.success('Success!');
                    } else {
                        this.$Message.error('Fail!');
                    }
                })
            },
            turnStatus () {
                this.pageStatus=!this.pageStatus;
            }
        }
    }
</script>

<style>
    #login-panel input{
        max-width:400px;
    }
</style>