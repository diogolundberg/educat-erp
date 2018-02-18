<template>
  <div class="border-bottom border-silver">
    <div class="p1 h4 pointer block flex justify-between"
      @click="show = !show">
      <div>{{ title }}</div>
      <div class="bold">{{ show ? '-' : '+' }}</div>
    </div>
    <transition enter-class="y0 op0" leave-to-class="y0 op0">
      <div v-if="show" class="p1 transition truncate">
        <slot />
      </div>
    </transition>
  </div>
</template>

<script>
  export default {
    name: "Accordion",
    props: {
      title: {
        type: String,
        required: true,
      },
    },
    data() {
      return {
        show: false,
      };
    },
  };
</script>
