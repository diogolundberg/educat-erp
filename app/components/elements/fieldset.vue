<template>
  <div>
    <h5 class="pb1 upcase border-silver border-bottom">{{ title }}</h5>
    <div class="py2">
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
      },
    },
  };
</script>
