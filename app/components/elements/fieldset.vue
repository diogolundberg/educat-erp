<template>
  <div>
    <h5 class="pb1 upcase border-gray border-bottom" v-if="title">
      {{ title }}
    </h5>
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
        required: true,
      },
      innerClass: {
        type: String,
        required: false,
        default: null,
      },
    },
  };
</script>
