<template>
  <div class="x3 y3 p10x circle bold h4 bg-gray white center shadow1">
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
