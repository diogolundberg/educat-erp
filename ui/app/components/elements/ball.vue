<template>
  <div class="x2 y2 p4x circle bold h4 center shadow0">
    <slot />
    {{ content }}
  </div>
</template>

<script>
  export default {
    name: "Ball",
    props: {
      content: {
        type: [Number, String],
        required: false,
        default: null,
      },
      blank: {
        type: Boolean,
        default: false,
        required: false,
      },
    },
  };
</script>
