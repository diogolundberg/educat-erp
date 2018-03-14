import Vue from "vue";
import * as Components from "./components/**/*.vue";
Object.values(Components).map(a => a.name && Vue.component(a.name, a));

import router from "./lib/router";

Vue.prototype.sleep = time => new Promise(r => setTimeout(r, time));

new Vue({ render: h => h("Application"), router }).$mount("main");
