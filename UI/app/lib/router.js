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
      component: lazy("Enrollment"),
      meta: { open: true },
      props: true,
    },
    {
      path: "/enrollments/:type",
      component: lazy("Enrollments"),
      meta: { header: true },
      props: true,
    },
    {
      path: "/enrollments/:type/:id",
      component: lazy("EnrollmentInfo"),
      meta: { header: true },
      props: true,
    },
    {
      path: "/onboarding",
      component: lazy("Onboarding"),
      meta: { header: true },
      props: true,
    },
    {
      path: "/onboardings",
      component: lazy("Onboardings"),
      meta: { header: true },
      props: true,
    },
    {
      path: "/onboardings/new",
      component: lazy("OnboardingItem"),
      meta: { header: true },
      props: true,
    },
    {
      path: "/onboardings/:id",
      component: lazy("OnboardingItem"),
      meta: { header: true },
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
