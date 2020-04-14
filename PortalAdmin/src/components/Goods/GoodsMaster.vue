<style></style>
<template>

    <div>
        <Form ref="formCustom" :model="formCustom" :rules="ruleCustom" :label-width="80">
            <FormItem label="Password" prop="passwd">
                <Input type="password" v-model="formCustom.passwd"></Input>
            </FormItem>
            <FormItem label="Confirm" prop="passwdCheck">
                <Input type="password" v-model="formCustom.passwdCheck"></Input>
            </FormItem>
            <FormItem label="Age" prop="age">
                <Input type="text" v-model="formCustom.age" number></Input>
            </FormItem>
            <FormItem>
                <Button type="primary" @click="handleSubmit('formCustom')">Submit</Button>
                <Button type="ghost" @click="handleReset('formCustom')" style="margin-left: 8px">Reset</Button>
            </FormItem>
        </Form>
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
  data() {
      const validatePass = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('Please enter your password'));
                } else {
                    if (this.formCustom.passwdCheck !== '') {
                        // 对第二个密码框单独验证
                        this.$refs.formCustom.validateField('passwdCheck');
                    }
                    callback();
                }
            };
            const validatePassCheck = (rule, value, callback) => {
                if (value === '') {
                    callback(new Error('Please enter your password again'));
                } else if (value !== this.formCustom.passwd) {
                    callback(new Error('The two input passwords do not match!'));
                } else {
                    callback();
                }
            };
            const validateAge = (rule, value, callback) => {
                if (!value) {
                    return callback(new Error('Age cannot be empty'));
                }
                // 模拟异步验证效果
                setTimeout(() => {
                    if (!Number.isInteger(value)) {
                        callback(new Error('Please enter a numeric value'));
                    } else {
                        if (value < 18) {
                            callback(new Error('Must be over 18 years of age'));
                        } else {
                            callback();
                        }
                    }
                }, 1000);
            };
    return {
        goodsEntity:{
            Name:'',
            Price:'',
            CoverPics:'',
            Detail:''
        },
        formCustom: {
                    passwd: '',
                    passwdCheck: '',
                    age: ''
                },
                ruleCustom: {
                    passwd: [
                        { validator: validatePass, trigger: 'blur' }
                    ],
                    passwdCheck: [
                        { validator: validatePassCheck, trigger: 'blur' }
                    ],
                    age: [
                        { validator: validateAge, trigger: 'blur' }
                    ]
                },
            title: "",
            content: "",
            editorOption: {},
            picToken: ""
    };
  },
  mounted() {
    this.$http.get("pic/token").then(res => {
      console.log(res);
    });
  },
  methods: {
    onEditorBlur: event => {},
    onEditorForcus: event => {},
    onEditorReady: event => {},
    handleSubmit (name) {
           this.$refs[name].validate((valid) => {
                    if (valid) {
                        this.$Message.success('Success!');
                    } else {
                        this.$Message.error('Fail!');
                    }
                })
            },
            handleReset (name) {
                this.$refs[name].resetFields();
            }
  }
};
</script>