<template>
  <span :is="container" class="raised">
    <Btn v-for="option in options" :key="option[idField]"
      @click="$emit('input', option[idField])" class="not-rounded"
      :class="value === option[idField] ? 'btn-primary' : 'bg-white black'"
      :label="option[labelField]" />
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
