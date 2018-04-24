<template>
  <div
    :class="`col-${size}`"
    class="pb1">
    <div class="h6">
      {{ title }}:
    </div>
    <div
      :title="value"
      class="h4 truncate">
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
      size: {
        type: Number,
        required: false,
        default: 4,
      },
    },
  };
</script>
