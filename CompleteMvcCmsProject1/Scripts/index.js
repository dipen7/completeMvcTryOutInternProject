import Vue from 'vue/dist/vue.js';
import VeeValidate from 'vee-validate';
import axios from 'axios';
import Quill from 'quill/core';
import ImageUploader from 'vue-image-upload-resize';

//import Toolbar from 'quill/modules/toolbar';
//import Snow from 'quill/themes/snow';
//Quill.register({
//    'modules/toolbar': Toolbar,
//    'themes/snow': Snow
//});

window.Quill = Quill;
window.Vue = Vue;
window.VeeValidate = VeeValidate;
window.axios = axios;
window.ImageUploader = ImageUploader;


//validation Extensions
VeeValidate.Validator.extend('otherThanZero', {
    getMessage: field => 'The ' + field + ' field is not selected.',
    validate(value, args) {
        if (value >= 1) {
            return true;
        }
        else {
            return false;
        }
    }
});