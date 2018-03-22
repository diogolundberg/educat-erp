import Vue from "vue";
import Snackbar from "snackbarlightjs";
import router from "./lib/router";
import store from "./lib/store";
import * as Components from "./components/**/*.vue";

Object.values(Components).map(a => a.name && Vue.component(a.name, a));

Vue.directive("el-focus", { inserted: (el) => el.focus() });
Vue.prototype.sleep = time => new Promise(r => setTimeout(r, time));
Vue.prototype.notify = msg => Snackbar.create(msg, { timeout: 1000 });

new Vue({ render: h => h("Application"), router, store }).$mount("main");
