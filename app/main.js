import Vue from "vue";
import * as Components from "./components/**/*.vue";

Object.values(Components).map(a => a.name && Vue.component(a.name, a));

new Vue({ render(h) { return h("App"); } }).$mount("main");
