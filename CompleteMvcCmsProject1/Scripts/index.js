import Vue from 'vue/dist/vue.js';
import VeeValidate from 'vee-validate';
import axios from 'axios';
import Multiselect from 'vue-multiselect';
import DataTable from 'vue-datatable';
//export default {
//    // OR register locally
//    components: { Multiselect },
//    data() {
//        return {
//            value: null,
//            options: ['list', 'of', 'options']
//        }
//    }
//};
//import Quill from 'quill/core';
//import ImageUploader from 'vue-image-upload-resize';

//import Toolbar from 'quill/modules/toolbar';
//import Snow from 'quill/themes/snow';
//Quill.register({
//    'modules/toolbar': Toolbar,
//    'themes/snow': Snow
//});

//window.Quill = Quill;

window.Vue = Vue;
window.VeeValidate = VeeValidate;
window.axios = axios;
window.Multiselect = Multiselect;
window.DataTable = DataTable;
//window.ImageUploader = ImageUploader;


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