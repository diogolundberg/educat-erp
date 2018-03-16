<template>
  <div>
    <div
      v-if="title"
      class="flex">
      <h3>{{ title }}</h3>
      <hr class="y0 ml2 flex-auto border-bottom">
    </div>
    <div
      :class="innerClass"
      class="py2">
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
