<template>
  <div class="my2">
    <div
      class="px2 py1 bg-silver gray pointer flex justify-between items-center"
      @click="show = !show">
      <div class="h6 upcase bold">{{ title }}</div>
      <div class="h4 bold">{{ show ? '-' : '+' }}</div>
    </div>
    <transition
      enter-class="op0"
      leave-to-class="op0">
      <div
        v-if="show"
        class="p2 ease truncate">
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
