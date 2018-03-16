<template>
  <span :is="container">
    <Btn
      v-for="option in options"
      :key="option[idField]"
      :class="{ 'btn-primary': value === option[idField] }"
      :label="option[labelField]"
      class="not-rounded rounded-bookend"
      @click="$emit('input', option[idField])" />
  </span>
</template>

<script>
  export default {
    name: "ButtonGroup",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
  };
</script>
