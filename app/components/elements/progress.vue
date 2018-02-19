<template>
  <span class="inline-block align-middle round50 bg-white shadow0 progress">
    <div class="bg-blue round50 h100 transition"
      :style="{ width }" />
  </span>
</template>

<script>
  export default {
    name: "Progress",
    props: {
      value: {
        type: Number,
        default: 0,
      },
      max: {
        type: Number,
        default: 100,
      },
    },
    computed: {
      width() {
        return this.value / this.max * 100 + "%";
      },
    },
  };
</script>
