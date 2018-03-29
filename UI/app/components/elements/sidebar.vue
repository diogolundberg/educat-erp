<template>
  <transition
    enter-class="left0"
    leave-to-class="left0">
    <aside
      class="fixed top-0 bottom-0 left-0 z4 bg-white shadow2 ease width-1">
      <div class="relative bg-green-grad">
        <div>
          <img
            src="../../assets/img/people.svg"
            class="circle x4 m2">
        </div>
        <div class="white px2 py1 truncate">{{ login }}</div>
      </div>
      <div>
        <div class="h-bg-silver">
          <a
            v-for="link in links"
            :key="link.url"
            :href="link.url"
            class="block p2 bold black truncate text-decoration-none">
              {{ link.name }}
          </a>
        </div>
      </div>
    </aside>
  </transition>
</template>

<script>
  export default {
    name: "Sidebar",
    props: {
      links: {
        type: Array,
        default: () => [],
      },
    },
    computed: {
      login() {
        return this.$store.getters.login;
      },
    },
  };
</script>
