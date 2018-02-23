<template>
  <div class="align-middle round50 bg-white shadow0 y1 p2x">
    <div class="bg-blue round50 h100 transition"
      :style="{ width }" />
  </div>
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
        const range = this.value / this.max;
        return `${range * 100}%`;
      },
    },
  };
</script>
