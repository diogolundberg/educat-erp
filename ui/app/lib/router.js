import Vue from "vue";
import VueRouter from "vue-router";

import store from "./store";

const lazy = name => () => Promise.resolve(Vue.component(name));

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  routes: [
    {
      path: "/",
      component: lazy("Login"),
      meta: { open: true },
    },
    {
      path: "/enroll/:id",
      component: lazy("Enroll"),
      meta: {
        open: true,
      },
      props: true,
    },
    {
      path: "/approvals",
      component: lazy("ListPage"),
      meta: {
        header: true,
        title: "Pendências",
        endpoint: "/v2/approvals",
        link: "/approvals",
        subkey: ["data"],
        columns: [
          { name: "name", title: "Nome" },
          { name: "email", title: "E-mail" },
          { name: "sentAt", title: "Status" },
        ],
      },
      props: true,
    },
    {
      path: "/approvals/:id",
      component: lazy("Approval"),
      meta: {
        header: true,
        title: "Pendências",
        endpoint: "/v2/approvals",
        type: "academic",
      },
      props: true,
    },
    {
      path: "/onboardings",
      component: lazy("ListPage"),
      meta: {
        header: true,
        title: "Onboardings",
        endpoint: "v2/Onboardings",
        link: "/onboardings",
        new: "/v2/onboardings/new",
        subkey: ["data"],
        columns: [
          { name: "semester", title: "Semestre" },
          { name: "year", title: "Ano" },
          { name: "startAt", title: "Inicio" },
          { name: "endAt", title: "Fim" },
          { name: "enrollmentCount", title: "Matrículas" },
        ],
      },
      props: true,
    },
    {
      path: "/onboardings/new",
      component: lazy("Onboarding"),
      meta: {
        header: true,
      },
      props: true,
    },
    {
      path: "/onboardings/:id",
      component: lazy("Onboarding"),
      meta: {
        header: true,
      },
      props: true,
    },

    {
      path: "/logout",
      beforeEnter(to, from, next) {
        delete localStorage.token;
        next("/");
      },
    },
    {
      path: "/403",
      component: lazy("NotAuthorized"),
      meta: { open: true },
    },
    {
      path: "*",
      component: lazy("NotFound"),
      meta: { open: true },
    },
  ],
});

router.beforeEach((to, from, next) => {
  if (!store.getters.logged && !to.meta.open) {
    next({ path: "/" });
  } else {
    next();
  }
});

export default router;
