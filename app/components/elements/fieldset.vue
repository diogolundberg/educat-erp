<template>
  <div>
    <h3 class="thin" v-if="title">
      {{ title }}
    </h3>
    <div class="py2" :class="innerClass">
      <slot />
    </div>
  </div>
</template>

<script>
  export default {
    name: "Fieldset",
    props: {
      title: {
        type: String,
        required: false,
        default: null,
      },
      innerClass: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
