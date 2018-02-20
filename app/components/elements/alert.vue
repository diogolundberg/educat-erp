<template>
  <div class="border-top-grey border4 p2 white mb2" :class="bgClass">
    {{ message }}
  </div>
</template>

<script>
  export default {
    name: "Alert",
    props: {
      message: {
        type: String,
        required: true,
      },
      type: {
        type: String,
        default: "info",
      },
    },
    data() {
      return {
        classes: {
          info: "bg-blue",
          success: "bg-green",
          error: "bg-red",
          warning: "bg-yellow",
        },
      };
    },
    computed: {
      bgClass() {
        return this.classes[this.type];
      },
    },
  };
</script>
