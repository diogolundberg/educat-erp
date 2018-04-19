<template>
  <div class="pb1">
    <div class="h6">
      {{ title }}:
    </div>
    <div class="h4">
      {{ value }}
    </div>
  </div>
</template>

<script>
  export default {
    name: "KeyValue",
    props: {
      value: {
        type: [String, Number],
        required: false,
        default: "",
      },
      title: {
        type: String,
        required: true,
      },
    },
  };
</script>
